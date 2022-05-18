# QBXML-Object-Models
Quickbooks C# strongly typed object models to generate QBXML without writing XML code and read QBXML responses back to strongly typed object models.
NOTE:  Uploaded a QbModels.QBO for generating XML and/or JSON for Quickbooks Online edition.  Documentation will follow.

This is part of a personal pet project I've been working on to help update and improve my C# coding skills and experience.  This dll allows me to generate the QBXML to call Quickbooks Desktop API without having to write XML code using the QbModels and QbModels.ENUM namespace.  This is done using 100% .netstandard2.0 calls.  There are no custom DLLs or other libraries used to generate the QBXML and/or process the results.  If you want to create QBXML and read responses using class objects instead of worrying about XML structures, I've created this tool to do so.
<br /><br/>
For example, the following C# code:
```csharp
using QbModels;

QbHelpers.XmlHelper.SetSdkVersion("13.0");
#region Create customer query request with a maximum of 50 responses
CustomerQueryRq customerRq = new() { MaxReturned = 50, Iterator = "Start" };
Console.WriteLine(customerRq.ToString());
#endregion
```
  
Produces the following output:
```xml
<?xml version="1.0" encoding="utf-16"?>
<?qbxml version="13.0"?>
<QBXML>
  <QBXMLMsgsRq onError="stopOnError">
    <CustomerQueryRq iterator="Start">
      <MaxReturned>50</MaxReturned>
    </CustomerQueryRq>
  </QBXMLMsgsRq>
</QBXML>
```

As of v1.2.0, I've refactored all the views to convert the response QBXML into a C# class object through the constructor (no more need to call the ToView extension) with the added benefit of reducing the dll size.  The class object can then be accessed and manipulated with LINQ or standard C# code.  The dll includes over 55 object views (ie AccountRs, InvoiceRs, CustomerRs, VendorRs, etc) that will convert the QBXML response into a C# object that can be accessed via C#.

```csharp
using QbModels;
using QbModels.ENUM;

AccountQueryRq accountsRq = new();
// QB.ExecuteQbRequest is from my personal class library and returns the QBXML from the RP Processor.  It can be found under UnitTests.
string qbRs = QB.ExecuteQbRequest(accountsRq); 
AccountRs accounts = new(qbRs);
if(accounts.StatusCode == "0" && accounts.Accounts.Count > 0)
{
  AccountRetDto account = accounts.Accounts.FirstOrDefault(a => a.AccountType == AccountType.AccountsPayable);
  AccountRetDto bank = accounts.Accounts.FirstOrDefault(a => a.AccountType == AccountType.Bank);
}
```

I've also added a couple of validation extensions using DataAnnotations and custom ValidationAttributes to be able to call IsEntityValid() and/or GetErrorsList() on the object.  This will help to notify you if your data is not valid.  This is not 100% but will warn you of a lot of different potential errors in your requests.  Also; these methods are optional and regardless of the results of the IsEntityValid, the .ToString() method will output the QBXML.  Personally, I use them during development to see where I may have added bad or incomplete data.

```csharp
if(customerRq.IsEntityValid())
{
  ...Your code here
}
else
{
  foreach(string s in customerRq.GetErrorsList())
  {
    ...Your code here
  }
}
```

I have objects for the majority of QBXML calls but have only used and tested a few since that's all I need.  Have re-designed the XML serialization and de-serialization process.  The result was running 125 internal unit tests in 1.2s to running 140 internal unit tests in 550ms.  See the Updates.md page for more updates.  The UnitTests folder contains the unit tests project I created to consume the QbModel library project and validate the results.  I also uploaded my personal request processor that I use to send/receive QBXML to/from the Quickbooks request processor.  This QbProcessor library is very limited but runs within a WPF/WinForm desktop application.  It is also a .netstandard2.0 dotnet library and will only work with a currently running Quickbooks Desktop application and opened Quickbooks desktop data file.  You can access this project at https://github.com/sflagp/QbProcessor.

I have covered the majority of QBXML requests in my unit testing but there are still bugs to be found.  If you do use this, please report these bugs so that I can look into why it's not working.  Look at the included unit test code I uploaded for testing the InvoiceQueryRq/InvoiceAddRq/InvoiceModRq QBXML calls to see how I use the dll to generate the QBXML and then convert the QBXML result string back into a C# object (in this case InvoiceRs).

Theoretically; since this is straight dotNet code and objects, this should also be compatible with VB but I haven't programmed in VB in many years so I haven't tested.
