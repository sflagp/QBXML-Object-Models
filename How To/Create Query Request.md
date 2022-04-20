# Creating a query request
The QbModels has the following paradigm.  For the most part, there should be one Rq for each QBXML request defined in the SDK. The request names 
should match the requests defined in the SDK.  At last count, I believe I have defined well over 100 query/add/mod requests.  Once the request is 
sent to the Quickbooks RP, the QBXML response can then be passed into the appropriate {request type}Rs.

For example, if you wanted to query Quickbooks for a customer, the SDK defines a CustomerQueryRq.  The QbModels contains a CustomerQueryRq
object that can be instantiated to build the QBXML.  The following will query Quickbooks for all customers whose name contains Smith.

```csharp
using QbModels.ENUM;

var qryRq = new CustomerQueryRq();
qryRq.NameFilter = new();
qryRq.NameFilter.Name = "Smith";
qryRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
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
var qryRq = new CustomerQueryRq() { NameFilter = new() { Name = "Smith", MatchCriterion = MatchCriterion.Contains } };
```

Matter of personal preference at that point.  

Now that the request is created, you can pass that QBXML to the Quickbooks RP.  You can use the ToString() extension `qryRq.ToString()` to pass the QBXML.
