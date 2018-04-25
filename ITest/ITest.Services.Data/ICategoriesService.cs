using ITest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITest.Services.Data
{
    public interface ICategoriesService
    {
        void Add(CategoryDTO dto);
        IEnumerable<CategoryDTO> GetAllCategories();
        int GetIdByCategoryName(string name);
       
    }
}
