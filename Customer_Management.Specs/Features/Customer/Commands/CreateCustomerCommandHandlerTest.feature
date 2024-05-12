Feature: Creating a Customer

A short summary of the feature

Scenario: Creating a new customer
    Given a customer repository
    And a new customer with details:
     | Id | FirstName | LastName | DateOfBirth | Email            | Phone       | BankAccountNumber |
     | 10 | John      | Doe      | 02/02/1992  | john@example.com | 09396080822 | 1987654321        |
    When the create customer command is handled
    Then the customer repository should add the new customer
    And the result should indicate success



