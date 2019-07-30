# PasswordStrengthAndBreachCheck
Step 1: Download/Clone the passwordStrengthAndBreachCheck by clicking on 'Clone and download' button on top right side of the code tab of github. 
Step 2: After downloading the solution to your local system, go to folder PasswordStrengthAndBreachCheck>passwordCheck open passwordCheck.sln in visual studio (use visual studio 2019, with netcoreapp2.1) 
Step 3: Build the solution, on successful build run the PasswordCheckAPI (don't close the browser) 
Step 4: Go to folder PasswordStrengthAndBreachCheck>ConsolePasswordCheck open ConsolePasswordCheck.sln in visual studio (use visual studio 2019, with netcoreapp2.1) 
Step 5: Build the solution, on successful build run the ConsolePasswordCheck, A console screen will open. Enter Username press enter and then Enter password you want to check the strength for. This Console app will return back the strength of the password in Blank, Short, Very Weak, Weak, Medium, Strong. 
Blank: If password is blank. 
Short: If Password length is less than 8. 
Very Weak: If password is at least 8 character long but it contains no number, no upper character and no special charater. 
Weak: If password is at least 8 character long, but Either Number or special character or upper character, 2 of them is missing. 
Medium: If password is at least 8 character long, but Either Number or special character or upper character, 1 of them is missing. 
Strong: when is 8 character long, contains at least one number, atleast one upper character and at least 1 special character. 
Step 6: Now, to test the different scenarios, Go to folder PasswordStrengthAndBreachCheck>StrengthAndBreachTest open StrengthAndBreachTest.sln passwordCheck.sln in visual studio (use visual studio 2019, with netcoreapp2.1) 
Step 7: Build the solution, on successful build go to the 'Test' tab on the top of the visual studio and click on run all tests. On completion it will show '8 tests passed'
 
