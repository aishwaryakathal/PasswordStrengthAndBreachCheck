using passwordCheck;
using System;
using passwordCheck.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace PasswordTest
{
    /// <summary>
    /// This test call successfully unit test all the scenarios realted to password strength and data breach
    /// </summary>
    [TestClass]
    public class PasswordTestScenarios
    {
        /// <summary>
        /// Test password to check if it is Blank
        /// </summary>
        [TestMethod]
        public void TestPasswordStrengthforBlank()
        {
            //Assemble the test to check the strength of password
            PasswordStrengthController PasswordStrengthCheck = new PasswordStrengthController();

            //Expected Result of the Test
            string expectedResult = "Password is Blank";

            //Actual Result of the Test for blank password
            string actualResult = PasswordStrengthCheck.StrengthCheck("");

            //Match expected result with actual result
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Test password to check if it is Short (Digits less than 8, also Do not have either numbers or any upper character or any special character : all 3 missing )
        /// </summary>
        [TestMethod]
        public void TestPasswordStrengthforShort()
        {
            //Assemble the test to check the strength of password
            PasswordStrengthController PasswordStrengthCheck = new PasswordStrengthController();

            //Expected Result of the Test
            string expectedResult = "Password is Short";

            //Actual Result of the Test for password "beautif" which doesnot satisfy all the requirements
            string actualResult = PasswordStrengthCheck.StrengthCheck("beautif");

            //Match expected result with actual result
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Test password to check if it is Very Weak (Do not have either numbers or any upper character or any special character : all 3 missing )
        /// </summary>
        [TestMethod]
        public void TestPasswordStrengthforVeryWeak()
        {
            //Assemble the test to check the strength of password
            PasswordStrengthController PasswordStrengthCheck = new PasswordStrengthController();

            //Expected Result of the Test
            string expectedResult = "Password is Very Weak";

            //Actual Result of the Test for password "beautiful1" which doesnot satisfy all the requirements
            string actualResult = PasswordStrengthCheck.StrengthCheck("beautiful");

            //Match expected result with actual result
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Test password to check if it is Weak (Do not have either numbers or any upper character or any special character : any 2 missing ) 
        /// </summary>
        [TestMethod]
        public void TestPasswordStrengthforWeak()
        {
            //Assemble the test to check the strength of password
            PasswordStrengthController PasswordStrengthCheck = new PasswordStrengthController();

            //Expected Result of the Test
            string expectedResult = "Password is Weak";

            //Actual Result of the Test for password "beautiful1" which doesnot satisfy all the requirements
            string actualResult = PasswordStrengthCheck.StrengthCheck("beautiful1");

            //Match expected result with actual result
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Test password to check if it is Medium (Do not have either numbers or any upper character or any special character : any 1 missing ) 
        /// </summary>
        [TestMethod]
        public void TestPasswordStrengthforMedium()
        {
            //Assemble the test to check the strength of password
            PasswordStrengthController PasswordStrengthCheck = new PasswordStrengthController();

            //Expected Result of the Test
            string expectedResult = "Password is Medium";

            //Actual Result of the Test for password "Beautiful1" which doesnot satisfy all the requirements
            string actualResult = PasswordStrengthCheck.StrengthCheck("Beautiful1");

            //Match expected result with actual result
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Test password to check if it is Strong (satisfy all the requirements)
        /// </summary>
        [TestMethod]
        public void TestPasswordStrengthforStrong()
        {
            //Assemble the test to check the strength of password
            PasswordStrengthController PasswordStrengthCheck = new PasswordStrengthController();

            //Expected Result of the Test
            string expectedResult = "Password is Strong";

            //Actual Result of the Test for password "Beautiful@1" which satisfy all the requirements
            string actualResult = PasswordStrengthCheck.StrengthCheck("Beautiful@1");

            //Match expected result with actual result
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Test data breach on the password
        /// </summary>
        /// <returns>Test Successful</returns>
        [TestMethod]
        public async Task TestPasswordDataBreach()
        {
            //Assemble the test to check the strength of password
            PasswordStrengthController PasswordDataBreachCheck = new PasswordStrengthController();

            //Expected Result of the Test
            string expectedResult = "84200";

            //Actual Result of the Test for data breach for password "pass123" is 84200
            string actualResult =await PasswordDataBreachCheck.PasswordBreachCheck("pass123");

            //Match expected result with actual result
            Assert.AreEqual(expectedResult, actualResult);
        }


        /// <summary>
        /// Test no data breach on the password
        /// </summary>
        /// <returns>Test Successful</returns>
        [TestMethod]
        public async Task TestPasswordNoDataBreach()
        {
            //Assemble the test to check the strength of password
            PasswordStrengthController PasswordDataBreachCheck = new PasswordStrengthController();

            //Expected Result of the Test is null
            string expectedResult = null;

            //Actual Result of the Test for data breach for password "S@12hb" is null
            string actualResult = await PasswordDataBreachCheck.PasswordBreachCheck("S@12hb");

            //Match expected result with actual result
            Assert.AreEqual(expectedResult, actualResult);
        }
    }    
}
