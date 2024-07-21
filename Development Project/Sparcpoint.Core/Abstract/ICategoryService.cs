using Sparcpoint.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparcpoint.Abstract
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        int AddCategory(Category category);
    }
}
