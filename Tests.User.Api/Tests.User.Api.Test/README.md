# User API Tests Refactoring Test

## Description

We would like you to refactor the UserControllerTests. 

## Limitations

Do NOT change the test expectations.

The tests must all pass.

## Changes Made

To avoid calling the DataBaseContext - created a mock test using the user service
Added extra check to validate/assert the expected results 

Most of the methods return an object of the user and changed the return to return oObject type.


To avoid taking time - I did not push for each change made - I see that as a bad practice
Something I do not do unless I am given a short time.