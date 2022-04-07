The QbModels has the following paradigm.  For the most part, there should be one Rq for each QBXML request defined in the SDK. The request names 
should match the requests defined in the SDK.  At last count, I believe I have defined well over 100 query/add/mod requests.  Once the request is 
sent to the Quickbooks RP, the QBXML response can then be passed into the appropriate Qb__View.

For example, if you wanted to query Quickbooks for a customer, the SDK defines a CustomerQueryRq.  The QbModels contains a CustomerQueryRq
object that can be instantiated to build the QBXML.  The following will query Quickbooks for all customers whose name contains Smith.

```csharp
var qryRq = new CustomerQueryRq();
qryRq.NameFilter = new();
qryRq.NameFilter.Name = "Smith";
qryRq.NameFilter.MatchCriterion = "Contains";
```

This will generate the following QBXML:

```xml
<?xml version="1.0" encoding="utf-16"?>
<?qbxml version="13.0"?>
<QBXML>
  <QBXMLMsgsRq onError="stopOnError">
    <CustomerQueryRq>
      <MaxReturned>99999</MaxReturned>
      <NameFilter>
        <MatchCriterion>Contains</MatchCriterion>
        <Name>Smith</Name>
      </NameFilter>
    </CustomerQueryRq>
  </QBXMLMsgsRq>
</QBXML>
```

Being C#, you can condense the code like this:

```csharp
var qryRq = new CustomerQueryRq() { NameFilter = new() { Name = "Smith", MatchCriterion = "Contains" } };
```

Matter of personal preference at that point.  

If you hover the mouse over the property, you should now see intellisense on the property.  
For example, if you hover over the property MatchCriterion, intellisense should show the following:

```
  Gets or sets the match criterion.
  MatchCriterion needs to be one of:
    StartsWith
    Contains
    EndsWith
  value
    The match criterion.
```

Now that the request is created, you can pass that QBXML to the Quickbooks RP.  You can use the ToString() extension `qryRq.ToString()` to pass the QBXML.
The RP will return a QBXML string that can then be passed into the appropriate Qb__View object.  There should be a Qb__View object for each request 
defined by the SDK.  One view can handle the Query/Add/Mod requests for the request type.  So in this instance, the correct view to use is QbCustomerView
(since it is a customer query request).  To read the result, just instantiate the view with the QBXML response. 

For example; if you assigned the QBXML response to qryRs, you would do the following:

```csharp
var rsView = new QbCustomersView(qryRs);
```

From there, you will have access to the response via object properties.  Each view should contain the following common properties:
// QBXML response attributes
StatusCode
StatusSeverity
StatusMessage
RemainingCount
IteratorID

//QBXML error response
ParseError
ParseErrorXml

Since this is a Customer request, the view will also have the properties:
Customers         // List of customers in the QBXML response
TotalCustomers    // Count of customers in the QBXML response

With the above information, you can now process the results.  

You can make sure the request ran correctly `rsView.StatusCode == "0"` or `rsView.StatusSeverity == "Info"`.  You can also check
to see how many customers were returned `rsView.TotalCustomers > 0`.  You can also iterate through each returned customer like:

```
foreach(CustomerRetDto c in rsView.Customers){
{
  Console.WriteLine($"Customer name is {c.FullName}";
}
