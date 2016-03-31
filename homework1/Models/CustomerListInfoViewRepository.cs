using System;
using System.Linq;
using System.Collections.Generic;
	
namespace homework1.Models
{   
	public  class CustomerListInfoViewRepository : EFRepository<CustomerListInfoView>, ICustomerListInfoViewRepository
	{

	}

	public  interface ICustomerListInfoViewRepository : IRepository<CustomerListInfoView>
	{

	}
}