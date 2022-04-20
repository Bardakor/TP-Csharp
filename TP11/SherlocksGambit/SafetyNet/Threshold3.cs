using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using SherlocksGambit.AI;
using SherlocksGambit.Game;
using SherlocksGambit.Utils.Helpers;

namespace SafetyNet
{
    [TestFixture]
    public class Threshold3
    {
        [Test]
        [TestCase("4/4/4/4/4/4/4/4", PlayerColor.Black, 0U)]
        [TestCase("4/4/4/4/4/4/4/4", PlayerColor.White, 0U)]
        [TestCase("KKKK/4/3M/3m/4/4/4/mmmm", PlayerColor.White, 1U)]
        [TestCase("KKKK/4/3M/3m/4/4/4/mmmm", PlayerColor.Black, 9U)]
        public void GeneratePathsTests(string fenCode, PlayerColor color, uint expected)
        {
            var board = new Board(fenCode);

            var method = typeof(AiBase).GetMethod("GeneratePaths", 
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            
            Assert.IsNotNull(method, "You probably modified the method's type, please restore it to its initial declaration");
            
            var paths = (List<PathObject>) method!.Invoke(null, new object?[]{board, color, false})!;
            
            Assert.IsNotNull(paths, "The list returned should not be null");
            Assert.AreEqual(paths!.Count, expected);
        }
    }
}