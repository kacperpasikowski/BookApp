using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.helpers
{
	public class ServiceResult
	{
		public bool Succeeded { get; private set; }
		public List<string> Errors { get; private set; }

		public ServiceResult(bool succeeded, List<string> errors)
		{
			Succeeded = succeeded;
			Errors = errors;
		}

		public static ServiceResult Success()
		{
			return new ServiceResult(true, null);
		}
		
		public static ServiceResult Failure(string error)
		{
			return new ServiceResult(false, new List<string> {error});
		}
		
		public static ServiceResult Failure(List<string> errors)
		{
			return new ServiceResult(false, errors);
		}
	}
}