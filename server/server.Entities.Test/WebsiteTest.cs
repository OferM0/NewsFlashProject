using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryAttribute = NUnit.Framework.CategoryAttribute;
using server.Model;
using Utilities;

namespace server.Entities.Test
{
    [TestFixture]
    internal class WebsiteTest
    {
        private WebSiteService websiteService;
        private Logger log;

        [SetUp]
        public void Init()
        {
            log = new Logger("Console");
            websiteService = new WebSiteService(log);
        }

        [Test, Category("TestClearList")]
        public void TestClearList()
        {
            try
            {
                int expectedCount = 0;

                List<WebSite> actualWebsites = websiteService.GetAllWebSites();
                int actualCount = actualWebsites.Count;

                Assert.AreEqual(expectedCount, actualCount);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestClearList failed: {ex.Message}");
            }
        }

        [Test, Category("TestGetAllWebsitees")]
        public void TestGetAllWebsitees()
        {
            try
            {
                string name = "new website";
                websiteService.AddNewWebSite(name);
                int expectedCount = 1;

                List<WebSite> actualWebsites = websiteService.GetAllWebSites();
                int actualCount = actualWebsites.Count;

                Assert.AreEqual(expectedCount, actualCount);

                int websiteId = MainManager.Instance.webSitesList.Last().Id; // get the ID of the last added RSS feed
                websiteService.DeleteWebSiteById(websiteId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestGetAllWebsites failed: {ex.Message}");
            }
        }

        [Test, Category("TestGetWebsiteById")]
        public void TestGetWebsiteById()
        {
            try
            {
                string name = "new website";
                websiteService.AddNewWebSite(name);
                string expectedName = "new website";

                int websiteId = MainManager.Instance.webSitesList.Last().Id; // get the ID of the last added RSS feed
                WebSite actualWebsite = websiteService.GetWebSiteById(websiteId);
                string actualName = actualWebsite.Name;

                Assert.AreEqual(expectedName, actualName);

                websiteService.DeleteWebSiteById(websiteId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestGetWebsiteById failed: {ex.Message}");
            }
        }

        [Test, Category("TestAddNewWebsite")]
        public void TestAddNewWebsite()
        {
            try
            {
                string name = "new website";
                websiteService.AddNewWebSite(name);
                int expectedCount = 1;

                List<WebSite> actualWebsites = websiteService.GetAllWebSites();
                int actualCount = actualWebsites.Count;

                Assert.AreEqual(expectedCount, actualCount);

                int websiteId = MainManager.Instance.webSitesList.Last().Id; // get the ID of the last added RSS feed
                websiteService.DeleteWebSiteById(websiteId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestAddNewWebsite failed: {ex.Message}");
            }
        }

        [Test, Category("TestUpdateWebsiteById")]
        public void TestUpdateWebsiteById()
        {
            try
            {
                string name = "new website";
                websiteService.AddNewWebSite(name);

                string updatedName = "updated website";
                int websiteId = MainManager.Instance.webSitesList.Last().Id; // get the ID of the last added RSS feed
                websiteService.UpdateWebSiteById(websiteId, updatedName);

                WebSite updatedWebsite = websiteService.GetWebSiteById(websiteId);

                Assert.AreEqual(updatedName, updatedWebsite.Name);

                websiteService.DeleteWebSiteById(websiteId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestUpdateWebsiteById failed: {ex.Message}");
            }
        }

        [Test, Category("TestDeleteWebsiteById")]
        public void TestDeleteWebsiteById()
        {
            try
            {
                string name = "new website";
                websiteService.AddNewWebSite(name);

                int websiteId = MainManager.Instance.webSitesList.Last().Id;
                websiteService.DeleteWebSiteById(websiteId);

                WebSite deletedWebsite = websiteService.GetWebSiteById(websiteId);

                Assert.IsNull(deletedWebsite);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestDeleteWebsiteById failed: {ex.Message}");
            }
        }
    }
}
