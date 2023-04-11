# User API Refactoring Test

## Description

We would like you to refactor the User API. 

Don't worry about breaking changes.

Think about clean code and architecture.

## Limitations

Do NOT change the fact that DatabaseContext uses InMemory database.

Note: A user is REQUIRED to have a first name, last name and age.

## Outside box thinking

Could create a baseEntity that could be used as a base class to pass User Id, createdOn, createdBy, updatedOn & updatedBy

Created UserDto - a good way of handling incoming requests which can be mapped to our models when returning a response.

Added Services to handle the requests - did not seem to be a good approach having the DataBaseContext declaration at the beginng of the api landing endpoint.

To avoid taking time - I did not push for each change made - I see that as a bad practice
Something I do not do unless I am given a short time.