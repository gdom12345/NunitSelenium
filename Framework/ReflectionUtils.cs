using NunitSelenium.Selenium.PageComponent;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Globalization;
using System.Reflection;

namespace NunitSelenium.Framework
{
    public class ReflectionUtils
    {
        public static void InitElements(object source, WebDriver driver)
        {
            System.Reflection.FieldInfo[] ps = source.GetType().GetFields();
            foreach (var item in ps)
            {
                string itemType = item.FieldType.Name;
                string itemType1 = item.GetType().FullName;
                Type type = item.GetType();

                if (item.FieldType.Name == "SeleniumElement")
                {
                    SeleniumElement seleniumElement = new SeleniumElement();
                    var attribute = Attribute.GetCustomAttribute(item, typeof(FindsByAttribute));

                    if (attribute == null)
                    {
                        continue;
                    }
                    FindsByAttribute findsBy = (FindsByAttribute)attribute;

                    var objInstance = Activator.CreateInstance(typeof(By),
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                        null, new object[] { findsBy.How.ToString() ?? "", findsBy.Using ?? "" },
                        CultureInfo.InvariantCulture);
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
    }
}
