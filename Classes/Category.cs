using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_project_2
{ 
    public class Category
    {
        public Category()
        {
        }

        public Category(string type)
        {
            Type = type;
        }

        public Category(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public int Id {get; set;}

        public string Name { get; set; }

        public string Type { get; set; }

        public CategoryText? CategoryTexts { get; set; }

        public List<CategoryProp>? CategoryProps { get; set; }

        public List<Inventory>? Inventories { get; set; }
    }

    public class CategoryText
    {
        public CategoryText()
        {
        }

        public CategoryText(int? category, string lang, string name, string extraInsertText, string extraText)
        {
            CategoryId = category;
            Lang = lang;
            Name = name;
            ExtraInsertText = extraInsertText;
            ExtraText = extraText;
        }

        public int Id { get; set; }

        [ForeignKey("Category.Id")]
        public int? CategoryId { get; set; }

        public string Lang { get; set; } = "sv";

        public string Name { get; set; }

        public string? ExtraInsertText { get; set; }

        public string? ExtraText { get; set; }

    }

    public class CategoryProp
    {
        public CategoryProp(Category category)
        {
        }

        public CategoryProp(int categoryId, string setting, string value) 
        {
            CategoryId = categoryId;
            Setting = setting;
            Value = value;            
        }

        
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public CategoryPropText? CategoryPropTexts { get; set; }

        public string Setting { get; set; }

        public string? Value { get; set; }        
    }

    public class CategoryPropText
    {
        public CategoryPropText()
        {
        }

        public CategoryPropText(int categoryPropId, string lang, string extraInsertText, string extraText)
        {
            CategoryPropId = categoryPropId;
            Lang = lang;
            ExtraInsertText = extraInsertText;
            ExtraText = extraText;
        }
        [Key]
        public int Id { get; set; }

        [ForeignKey("CategoryProp.Id")]
        public int? CategoryPropId { get; set; }

        public string Lang { get; set; } = "sv";

        public string? ExtraInsertText { get; set; }

        public string? ExtraText { get; set; }

    }
}
