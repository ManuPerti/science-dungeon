using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class MathsManagerTests
    {
        [TestMethod()]
        public void RandomIntTest()
        {
            var mathsManager = new MathsManager();
            int[] operation = { 0, 1, 2, 3 };
            bool isInRange = false;
            /*for(int i = 0; i < operation.Length; i++)
            {
                switch (operation[i])
                {
                    case 0:
                        int randomNumber = MathsManager.RandomInt(operation[i]);
                        isInRange = (randomNumber >= 0 && randomNumber <= 90);
                        break;
                }
            }*/

            int randomNumber = mathsManager.RandomInt(0);
            isInRange = (randomNumber >= 0 && randomNumber < 90);

            Assert.IsTrue(isInRange);
        }
    }
}