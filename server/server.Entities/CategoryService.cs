using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using Utilities;
using server.Dal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace server.Entities
{
    public class CategoryService : BaseEntity
    {
        public CategoryService(Logger log) : base(log)
        {
        }

        public void ClearList()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute ClearList function in Categories Entity." });

                MainManager.Instance.categoriesList.Clear();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute ClearList function in Categories Entity, {ex.Message}." });

                throw;
            }
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetAllCategories function in Categories Entity." });

                MainManager.Instance.categoriesList = MainManager.Instance.db.Categories.ToList();
                return MainManager.Instance.categoriesList;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetAllCategories function in Categories Entity, {ex.Message}." });

                throw;
            }
        }

        public Category GetCategoryById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute GetCategoryById(id:{id}) function in Categories Entity." });

                Category category = MainManager.Instance.db.Categories.Find(id);
                return category;
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute GetCategoryById(id:{id}) function in Categories Entity, {ex.Message}." });

                throw;
            }
        }

        public void AddNewCategory(string topic)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute AddNewCategory function in Categories Entity." });

                Category category = new Category
                {
                    Topic = topic
                };
                MainManager.Instance.categoriesList.Add(category);
                MainManager.Instance.db.Categories.Add(category);
                MainManager.Instance.db.SaveChanges();
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute AddNewCategory function in Categories Entity, {ex.Message}." });

                throw;
            }
        }

        public void UpdateCategoryById(int id, string topic)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute UpdateCategoryById(id:{id}) function in Categories Entity." });

                Category category = MainManager.Instance.db.Categories.Find(id);
                if (category != null)
                {
                    category.Topic = topic;
                    MainManager.Instance.categoriesList[id] = category;
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute UpdateCategoryById(id:{id}) function in Categories Entity, {ex.Message}." });

                throw;
            }
        }

        public void DeleteCategoryById(int id)
        {
            try
            {
                MainManager.Instance.log.LogEvent(new LogItem { LogTime = DateTime.Now, Type = "Event", Message = $"Execute DeleteCategoryById(id:{id}) function in Categories Entity." });

                Category category = MainManager.Instance.db.Categories.Find(id);
                if (category != null)
                {
                    MainManager.Instance.categoriesList.Remove(category);
                    MainManager.Instance.db.Categories.Remove(category);
                    MainManager.Instance.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.log.LogError(new LogItem { LogTime = DateTime.Now, Type = "Error", Message = $"Failed to execute DeleteCategoryById(id:{id}) function in Categories Entity, {ex.Message}." });

                throw;
            }
        }
    }
}
