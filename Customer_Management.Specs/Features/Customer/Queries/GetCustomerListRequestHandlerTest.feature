Feature: GetCustomerListRequestHandlerTest

A short summary of the feature

Scenario: Get all Customers
    Given a customer repository is available
    When I request all customers
    Then all customers should be returned