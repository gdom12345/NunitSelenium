namespace NunitSelenium.Selenium
{
    public class TableResults
    {
        public List<Dictionary<string, object>> dataSet { get; }

        public TableResults(List<Dictionary<string, object>> dataSet)
        {
            this.dataSet = dataSet;
        }


    }
}
