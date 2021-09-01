using System.Collections.Generic;
using System.Linq;

namespace Dicer.Tests.Helpers
{
	public class InfiniteQueue
	{
		private readonly Queue<int> _queue;
		private int _last = 0;

		public InfiniteQueue(params int[] numbers)
		{
			_queue = new(numbers);
		}

		public int Dequeue()
		{
			if (_queue.Any())
			{
				_last = _queue.Dequeue();
			}

			return _last;
		}
	}
}