using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using server.Entities;
using CategoryAttribute = NUnit.Framework.CategoryAttribute;
using server.Model;
using Utilities;

namespace server.Entities.Test
{
    [TestFixture]
    internal class NewsItemTest
    {
        private NewsItemService newsItemService;
        private Logger log;

        [SetUp]
        public void Init()
        {
            log = new Logger("Console");
            newsItemService = new NewsItemService(log);
        }

        [Test, Category("TestClearList")]
        public void TestClearList()
        {
            try
            {
                int expectedCount = 0;

                List<NewsItem> actualNewsItems = newsItemService.GetAllNewsItems();
                int actualCount = actualNewsItems.Count;

                Assert.AreEqual(expectedCount, actualCount);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestClearList failed: {ex.Message}");
            }
        }

        [Test, Category("TestGetAllNewsItems")]
        public void TestGetAllNewsItems()
        {
            try
            {
                string itemId = "123";
                string title = "Test news title";
                string description = "Test news description";
                string link = "http://example.com/news/123";
                string imageUrl = "http://example.com/news/123/image.jpg";
                DateTime publishDate = DateTime.Now;
                int categoryId = 1;
                int websiteId = 2;

                newsItemService.AddNewNewsItem(itemId, title, description, link, imageUrl, publishDate, categoryId, websiteId);
                int expectedCount = 1;

                List<NewsItem> actualNewsItems = newsItemService.GetAllNewsItems();
                int actualCount = actualNewsItems.Count;

                Assert.AreEqual(expectedCount, actualCount);

                int newsItemId = MainManager.Instance.newsItemsList.Last().Id;
                newsItemService.DeleteNewsItemById(newsItemId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestGetAllNewsItems failed: {ex.Message}");
            }
        }

        [Test, Category("TestGetNewsItemById")]
        public void TestGetNewsItemById()
        {
            try
            {
                string itemId = "123";
                string title = "Test news title";
                string description = "Test news description";
                string link = "http://example.com/news/123";
                string imageUrl = "http://example.com/news/123/image.jpg";
                DateTime publishDate = DateTime.Now;
                int categoryId = 1;
                int websiteId = 2;

                newsItemService.AddNewNewsItem(itemId, title, description, link, imageUrl, publishDate, categoryId, websiteId);
                string expectedTitle = "John Doe";

                int newsItemId = MainManager.Instance.newsItemsList.Last().Id;
                NewsItem actualNewsItem = newsItemService.GetNewsItemById(newsItemId);
                string actualTitle = actualNewsItem.Title;

                Assert.AreEqual(expectedTitle, actualTitle);

                newsItemService.DeleteNewsItemById(newsItemId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestGetNewsItemById failed: {ex.Message}");
            }
        }

        [Test, Category("TestAddNewNewsItem")]
        public void TestAddNewNewsItem()
        {
            try
            {
                string itemId = "123";
                string title = "Test news title";
                string description = "Test news description";
                string link = "http://example.com/news/123";
                string imageUrl = "http://example.com/news/123/image.jpg";
                DateTime publishDate = DateTime.Now;
                int categoryId = 1;
                int websiteId = 2;

                newsItemService.AddNewNewsItem(itemId, title, description, link, imageUrl, publishDate, categoryId, websiteId);
                int expectedCount = 1;

                List<NewsItem> actualNewsItems = newsItemService.GetAllNewsItems();
                int actualCount = actualNewsItems.Count;

                Assert.AreEqual(expectedCount, actualCount);

                int newsItemId = MainManager.Instance.newsItemsList.Last().Id;
                newsItemService.DeleteNewsItemById(newsItemId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestAddNewNewsItem failed: {ex.Message}");
            }
        }

        [Test, Category("TestUpdateNewsItemById")]
        public void TestUpdateNewsItemById()
        {
            try
            {
                string itemId = "123";
                string title = "Test news title";
                string description = "Test news description";
                string link = "http://example.com/news/123";
                string imageUrl = "http://example.com/news/123/image.jpg";
                DateTime publishDate = DateTime.Now;
                int categoryId = 1;
                int websiteId = 2;

                newsItemService.AddNewNewsItem(itemId, title, description, link, imageUrl, publishDate, categoryId, websiteId);

                int updatedClick = 1;
                newsItemService.UpdateNewsItemById(itemId, updatedClick);

                int newsItemId = MainManager.Instance.newsItemsList.Last().Id;
                NewsItem updatedNewsItem = newsItemService.GetNewsItemById(newsItemId);

                Assert.AreEqual(updatedClick, updatedNewsItem.ClickCount);

                newsItemService.DeleteNewsItemById(newsItemId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestUpdateNewsItemById failed: {ex.Message}");
            }
        }

        [Test, Category("TestDeleteNewsItemById")]
        public void TestDeleteNewsItemById()
        {
            try
            {
                string itemId = "123";
                string title = "Test news title";
                string description = "Test news description";
                string link = "http://example.com/news/123";
                string imageUrl = "http://example.com/news/123/image.jpg";
                DateTime publishDate = DateTime.Now;
                int categoryId = 1;
                int websiteId = 2;

                newsItemService.AddNewNewsItem(itemId, title, description, link, imageUrl, publishDate, categoryId, websiteId);

                int newsItemId = MainManager.Instance.newsItemsList.Last().Id;
                newsItemService.DeleteNewsItemById(newsItemId);
                NewsItem deletedNewsItem = newsItemService.GetNewsItemById(newsItemId);

                Assert.IsNull(deletedNewsItem);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestDeleteNewsItemById failed: {ex.Message}");
            }
        }
    }
}
