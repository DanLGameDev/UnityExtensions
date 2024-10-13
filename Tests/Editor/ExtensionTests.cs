using UnityEngine;
using NUnit.Framework;

namespace DGP.UnityExtensions.Editor.Tests
{
    public class ExtensionTests
    {
        //Vector3 tests
        [Test]
        public void Vector3Extensions_With()
        {
            Vector3 vector = new Vector3(1, 2, 3);
            Vector3 newVector = vector.With(x: 4, z: 5);
            Assert.AreEqual(new Vector3(4, 2, 5), newVector);
            
            newVector = vector.With(y: 6);
            Assert.AreEqual(new Vector3(1, 6, 3), newVector);
        }
    }
}