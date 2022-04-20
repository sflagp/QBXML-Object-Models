# Handling QBXML responses

When you send a QBXML request to the Quickbooks Request Processor, the RP will return a QBXML response string.  The string response can then be passed into the appropriate *{request type}Rs* object.  There should be a *{request type}Rs* object for each request defined in QbModels.  One view can handle the Query/Add/Mod results for the request type.  So in this instance, the correct view to use is CustomerRs
(since it is a customer query request).  To read the result, just instantiate the view with the QBXML response. 

For example; if the QBXML response string is assigned to a string variable qryRs, you would do the following:

```csharp
CustomerRs custRs = new(qryRs);
```

From there, you will have access to the response via object properties.  Each view should contain the following common properties:
```
// QBXML response attributes
StatusCode
StatusSeverity
StatusMessage
RemainingCount
IteratorID

//QBXML error response
ParseError
ParseErrorXml
```

Since this is a Customer request, the view will also have the properties:
```
Customers         // List of customers in the QBXML response
TotalCustomers    // Count of customers in the QBXML response
```

With the above information, you can now process the results.  

You can make sure the request ran correctly `rsView.StatusCode == "0"` or `rsView.StatusSeverity == "Info"`.  You can also check
to see how many customers were returned `rsView.TotalCustomers > 0`.  You can also iterate through each returned customer like:

```
foreach(CustomerRetDto c in rsView.Customers){
{
  Console.WriteLine($"Customer name is {c.FullName}";
}
