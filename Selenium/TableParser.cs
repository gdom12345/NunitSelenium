using OpenQA.Selenium;

namespace NunitSelenium.Selenium
{
    internal class TableParser
    {
        private WebDriver driver;

        public TableParser(WebDriver driver)
        {
            this.driver = driver;
        }

        public List<Dictionary<string, object>> ParseTable(string cellsXPath, string headersXPath)
        {
            List<IWebElement> headerElements =
                new List<IWebElement>(driver.FindElements(By.XPath(headersXPath)));
            List<string> headers = new List<string>();
            headerElements.ForEach(header => headers.Add(header.Text));
            return ParseTable(cellsXPath, headers.ToArray());
        }

        public List<Dictionary<string, object>> ParseTable(string cellsXPath, string[] headers)
        {
            List<IWebElement> tableCells = new List<IWebElement>(
                driver.FindElements(By.XPath(cellsXPath)));
            return ParseTable(tableCells, headers);

        }

        public List<Dictionary<string, object>> ParseTable(List<IWebElement> tableCells,
            string[] headers)
        {
            List<Dictionary<string, object>> table = new List<Dictionary<string, object>>();
            for (int i = 0; i < tableCells.Count;)
            {
                Dictionary<string, object> tableRow = null;
                for (int j = 0; j < headers.Length; j++, i++)
                {
                    if (j == 0)
                    {
                        tableRow = new Dictionary<string, object>();
                        table.Add(tableRow);
                    }
                    tableRow.Add(headers[j], tableCells[i].Text);
                }
            }

            return table;
        }
    }
}
