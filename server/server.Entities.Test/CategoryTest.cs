using NUnit.Framework;
using server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using CategoryAttribute = NUnit.Framework.CategoryAttribute;

namespace server.Entities.Test
{
    [TestFixture]
    internal class CategoryTest
    {
        private CategoryService categoryService;
        private Logger log;

        [SetUp]
        public void Init()
        {
            log = new Logger("Console");
            categoryService = new CategoryService(log);
        }

        [Test, Category("TestClearList")]
        public void TestClearList()
        {
            try
            {
                int expectedCount = 0;

                List<Category> actualCategories = categoryService.GetAllCategories();
                int actualCount = actualCategories.Count;

                Assert.AreEqual(expectedCount, actualCount);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestClearList failed: {ex.Message}");
            }
        }

        [Test, Category("TestGetAllCategories")]
        public void TestGetAllCategories()
        {
            try
            {
                string name = "new category";
                categoryService.AddNewCategory(name);
                int expectedCount = 1;

                List<Category> actualCategories = categoryService.GetAllCategories();
                int actualCount = actualCategories.Count;

                Assert.AreEqual(expectedCount, actualCount);

                int categoryId = MainManager.Instance.categoriesList.Last().Id; // get the ID of the last added RSS feed
                categoryService.DeleteCategoryById(categoryId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestGetAllCategories failed: {ex.Message}");
            }
        }

        [Test, Category("TestGetCategoryById")]
        public void TestGetCategoryById()
        {
            try
            {
                string name = "new category";
                categoryService.AddNewCategory(name);
                string expectedName = "new category";

                int categoryId = MainManager.Instance.categoriesList.Last().Id; // get the ID of the last added RSS feed
                Category actualCategory = categoryService.GetCategoryById(categoryId);
                string actualName = actualCategory.Topic;

                Assert.AreEqual(expectedName, actualName);

                categoryService.DeleteCategoryById(categoryId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestGetCategoryById failed: {ex.Message}");
            }
        }

        [Test, Category("TestAddNewCategory")]
        public void TestAddNewCategory()
        {
            try
            {
                string name = "new category";
                categoryService.AddNewCategory(name);
                int expectedCount = 1;

                List<Category> actualCategories = categoryService.GetAllCategories();
                int actualCount = actualCategories.Count;

                Assert.AreEqual(expectedCount, actualCount);

                int categoryId = MainManager.Instance.categoriesList.Last().Id; // get the ID of the last added RSS feed
                categoryService.DeleteCategoryById(categoryId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestAddNewCategory failed: {ex.Message}");
            }
        }

        [Test, Category("TestUpdateCategoryById")]
        public void TestUpdateCategoryById()
        {
            try
            {
                string name = "new category";
                categoryService.AddNewCategory(name);

                string updatedName = "updated category";
                int categoryId = MainManager.Instance.categoriesList.Last().Id; // get the ID of the last added RSS feed
                categoryService.UpdateCategoryById(categoryId, updatedName);

                Category updatedCategory = categoryService.GetCategoryById(categoryId);

                Assert.AreEqual(updatedName, updatedCategory.Topic);

                categoryService.DeleteCategoryById(categoryId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestUpdateCategoryById failed: {ex.Message}");
            }
        }

        [Test, Category("TestDeleteCategoryById")]
        public void TestDeleteCategoryById()
        {
            try
            {
                string name = "new category";
                categoryService.AddNewCategory(name);

                int categoryId = MainManager.Instance.categoriesList.Last().Id;
                categoryService.DeleteCategoryById(categoryId);

                Category deletedCategory = categoryService.GetCategoryById(categoryId);

                Assert.IsNull(deletedCategory);
            }
            catch (Exception ex)
            {
                Assert.Fail($"TestDeleteCategoryById failed: {ex.Message}");
            }
        }
    }
}
