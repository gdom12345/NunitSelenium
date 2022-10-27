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
                    //Clean this up. Shouldn't need to get attribute and then later method
                    CustomAttributeData attribute = new List<CustomAttributeData>
                         (item.CustomAttributes).Where(member => member.AttributeType.Name
                         == "FindsByAttribute").First();

                    if (attribute == null)
                    {
                        continue;
                    }
                    FindsByAttribute MyAttribute = (FindsByAttribute)Attribute
                        .GetCustomAttribute(item, typeof(FindsByAttribute));

                    By by = (By)Activator.CreateInstance(typeof(By), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                        null, new object[] { MyAttribute.How.ToString(), MyAttribute.Using },
                        CultureInfo.InvariantCulture);
                    seleniumElement.driver = driver;
                    seleniumElement.by = by;
                    item.SetValue(source, seleniumElement);

                }

            }
        }
    }
}
