using Xunit;

namespace FromCenter.Tests
{
    public class FromCenterTests
    {
        [Theory]
        [InlineData(new int[] {
            1, 2, 3, 4, 5, 6, 7
        }, new int[] {
            4, 5, 3, 6, 2, 7, 1
        }, true)]
        [InlineData(new int[] {
            1, 2, 3, 4, 5, 6, 7
        }, new int[] {
            4, 3, 5, 2, 6, 1, 7
        }, false)]
        [InlineData(new int[] {
            1, 2, 3, 4, 5, 6
        }, new int[] {
            3, 4, 2, 5, 1, 6
        }, true)]
        [InlineData(new int[] {
            1, 2, 3, 4, 5, 6
        }, new int[] {
            4, 3, 5, 2, 6, 1
        }, false)]
        public void SimpleNumberIteration(int[] enumerable, int[] expected, bool startFromRight)
        {
            Assert.Equal(expected, enumerable.FromCenter(startFromRight));
        }
    }
}
