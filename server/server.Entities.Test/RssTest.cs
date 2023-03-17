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
    internal class RssTest
    {
        private RssService rssService;
        private Logger log;

        [SetUp]
        public void Init()
        {
            log = new Logger("Console");
            rssService = new RssService(log);
        }

        [Test, Category("TestClearList")]
        public void TestClearList()
        {
            try
            {
                int expectedCount = 0;

                List<Rss> actualRsses = rssService.GetAllRsses();
                int actualCount = actualRsses.Count;

                Assert.AreEqual(expectedCount, actualCount);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestClearList failed: {ex.Message}");
            }
        }

        [Test, Category("TestGetAllRsses")]
        public void TestGetAllRsses()
        {
            try
            {
                string link = "http://example.com/feed";
                int categoryId = 1;
                int websiteId = 2;

                rssService.AddNewRss(link, categoryId, websiteId);
                int expectedCount = 1;

                List<Rss> actualRsss = rssService.GetAllRsses();
                int actualCount = actualRsss.Count;

                Assert.AreEqual(expectedCount, actualCount);

                int rssId = MainManager.Instance.rssesList.Last().Id; // get the ID of the last added RSS feed
                rssService.DeleteRssById(rssId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestGetAllRsss failed: {ex.Message}");
            }
        }

        [Test, Category("TestGetRssById")]
        public void TestGetRssById()
        {
            try
            {
                string link = "http://example.com/feed";
                int categoryId = 1;
                int websiteId = 2;

                rssService.AddNewRss(link, categoryId, websiteId);
                string expectedLink = "http://example.com/feed";

                int rssId = MainManager.Instance.rssesList.Last().Id; // get the ID of the last added RSS feed
                Rss actualRss = rssService.GetRssById(rssId);
                string actualLink = actualRss.Url;

                Assert.AreEqual(expectedLink, actualLink);

                rssService.DeleteRssById(rssId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestGetRssById failed: {ex.Message}");
            }
        }

        [Test, Category("TestAddNewRss")]
        public void TestAddNewRss()
        {
            try
            {
                string link = "http://example.com/feed";
                int categoryId = 1;
                int websiteId = 2;

                rssService.AddNewRss(link, categoryId, websiteId);
                int expectedCount = 1;

                List<Rss> actualRsss = rssService.GetAllRsses();
                int actualCount = actualRsss.Count;

                Assert.AreEqual(expectedCount, actualCount);

                int rssId = MainManager.Instance.rssesList.Last().Id; // get the ID of the last added RSS feed
                rssService.DeleteRssById(rssId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestAddNewRss failed: {ex.Message}");
            }
        }

        [Test, Category("TestUpdateRssById")]
        public void TestUpdateRssById()
        {
            try
            {
                string link = "http://example.com/feed";
                int categoryId = 1;
                int websiteId = 2;
                rssService.AddNewRss(link, categoryId, websiteId);

                string updatedLink = "http://example2222.com/feed";
                int rssId = MainManager.Instance.rssesList.Last().Id; // get the ID of the last added RSS feed
                rssService.UpdateRssById(rssId, updatedLink, categoryId, websiteId);

                Rss updatedRss = rssService.GetRssById(rssId);

                Assert.AreEqual(updatedLink, updatedRss.Url);

                rssService.DeleteRssById(rssId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestUpdateRssById failed: {ex.Message}");
            }
        }

        [Test, Category("TestDeleteRssById")]
        public void TestDeleteRssById()
        {
            try
            {
                string link = "http://example.com/feed";
                int categoryId = 1;
                int websiteId = 2;
                rssService.AddNewRss(link, categoryId, websiteId);

                int rssId = MainManager.Instance.rssesList.Last().Id;
                rssService.DeleteRssById(rssId);

                Rss deletedRss = rssService.GetRssById(rssId);

                Assert.IsNull(deletedRss);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestDeleteRssById failed: {ex.Message}");
            }
        }
    }
}
