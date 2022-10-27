using CsvHelper;
using CsvHelper.Configuration;
using NunitSelenium.Selenium.PageComponent;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Globalization;
using System.Reflection;

namespace NunitSelenium.Framework
{
    public static class ReflectionUtils
    {
        public static Object GetObject(this Dictionary<string, object> dict, Type type)
        {
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                var prop = type.GetProperty(kv.Key);
                if (prop == null) continue;

                object value = kv.Value;
                if (value is Dictionary<string, object>)
                {
                    value = GetObject(dict, prop.PropertyType); // <= This line
                }

                prop.SetValue(obj, value, null);
            }
            return obj;
        }

        public static T GetObject<T>(this Dictionary<string, object> dict)
        {
            return (T)GetObject(dict, typeof(T));
        }

        public static List<T> GetList<T>(this List<Dictionary<string, object>> dictList)
        {
            List<T> list = new List<T>();
            foreach (Dictionary<string, object> dict in dictList)
            {
                list.Add(GetObject<T>(dict));
            }

            return list;
        }

        public static void InitElements(object source, WebDriver driver)
        {
            FieldInfo[] ps = source.GetType().GetFields();
            foreach (var item in ps)
            {

                if (item.FieldType.Name == "SeleniumElement")
                {
                    SeleniumElement seleniumElement = new SeleniumElement();
                    var attribute = Attribute.GetCustomAttribute(item, typeof(FindsByAttribute));

                    if (attribute == null)
                    {
                        continue;
                    }
                    FindsByAttribute findsBy = (FindsByAttribute)attribute;

                    var objInstance = GetBy(findsBy.How, findsBy.Using);
                    if (objInstance == null)
                    {
                        continue;
                    }
                    By by = (By)objInstance;

                    seleniumElement.driver = driver;
                    seleniumElement.by = by;
                    item.SetValue(source, seleniumElement);

                }

            }
        }

        private static By GetBy(How how, string value)
        {
            //I should replace this with reflection
            switch (how)
            {
                case How.Id:
                    return By.Id(value);
                case How.Name:
                    return By.Name(value);
                case How.ClassName:
                    return By.ClassName(value);
                case How.TagName:
                    return By.TagName(value);
                case How.LinkText:
                    return By.LinkText(value);
                case How.PartialLinkText:
                    return By.PartialLinkText(value);
                case How.CssSelector:
                    return By.CssSelector(value);
                case How.XPath:
                    return By.XPath(value);
                default:
                    throw new Exception("Did NOT find By Locator type of " + how);
            }
        }

        //Genericize this and ship it to Utility class
        public static List<T> getListFromCsvFile<T>(string resourceFileName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            using (var reader = new StreamReader(FileUtils.getResourcesFolder() + "\\" + resourceFileName))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<T>();
                return new List<T>(records);
            }
        }
    }
}
