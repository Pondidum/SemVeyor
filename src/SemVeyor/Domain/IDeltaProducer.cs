using System.Collections.Generic;

namespace SemVeyor.Domain
{
	public interface IDeltaProducer<T>
	{
		string Name { get; }
		IEnumerable<object> UpdatedTo(T newer);
	}
}
