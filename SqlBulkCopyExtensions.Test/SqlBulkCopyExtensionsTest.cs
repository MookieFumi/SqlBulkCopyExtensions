using System.Configuration;
using System.Data;
using NUnit.Framework;
using SqlBulkCopyExtensions.Test.Model;

namespace SqlBulkCopyExtensions.Test
{
    [TestFixture]
    public class SqlBulkCopyExtensionsTest
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ModelContext"].ConnectionString;
        private readonly ModelContext _dbContext = TestHelper.GetDbContext();

        [TestFixtureSetUp]
        public void SetUp()
        {
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
        }

        public void When_GetDataTable_is_called_all_items_are_included()
        {
            // Arrange
            var accessLogList = TestHelper.GenerateAccessLogList(150);
            var sqlBulkCopy = new System.Data.SqlClient.SqlBulkCopy(ConnectionString);

            // Act
            DataTable dataTable = sqlBulkCopy.PrepareDataTable(_dbContext, accessLogList, ConnectionString);

            // Assert
            Assert.AreEqual(dataTable.Rows.Count, 150);
        }
    }
}
