using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProductNotFoundExceptions(int id) 
        : NotFoundException($"product with id {id} not found !!")
    {
    }
}
