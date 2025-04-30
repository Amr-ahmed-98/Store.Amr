using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderNotFoundExceptions(Guid id) :
        NotFoundException($"order with id {id} not found !!")
    {
    }
}
