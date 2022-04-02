# QBXML-Object-Models
Quickbooks C# object models to generate QBXML without writing XML code

This is part of a personal pet project I've been working on to help update and improve my C# coding skills and experience.  This dll allows me to generate the QBXML to call Quickbooks Desktop API without having to write XML code.
<br /><br/>
For example, the following C# code:
```
#region Create customer query request with a maximum of 50 responses
CustomerQueryRq customerRq = new() { MaxReturned = 50, Iterator = "Start" };
Console.WriteLine(customerRq.ToString());
#endregion
```
  
Produces the following output:
```
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

I've also added a one line command to convert the response QBXML into a C# class object that can be accessed and manipulated with Linq or standard C# code.

```
AccountQueryRq accountsRq = new();
string qbRs = QB.ExecuteQbRequest(accountsRq); // QB.ExecuteQbRequest is from my personal class library
QbAccountsView accounts = QbFunctions.ToView<QbAccountsView>(qbRs);
if(accounts.StatusCode == "0" && accounts.Accounts.Count > 0)
{
  AccountRetDto account = accounts.Accounts.FirstOrDefault(a => a.AccountType == "AccountsPayable");
  AccountRetDto bank = accounts.Accounts.FirstOrDefault(a => a.AccountType == "Bank");
}
```

I've also added a couple of validation extensions using DataAnnotations and custom ValidationAttributes to be able to call IsEntityValid() and/or GetErrorsList() on the object.  This will help to notify you if your data is not valid.  This is not 100% but will warn you of a lot of different potential errors in your requests.

``` 
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

I have objects for the majority of QBXML calls but have only used and tested a few since that's all I need.  While it is not complete, I am continuing to make updates and changes.  Have completed the majority of the Unit Tests for generating the QBXML as well as sending the QBXML to the request processor.  I have covered the majority of QBXML requests in my unit testing but there are still bugs to be found.  If you do use this, please report these bugs so that I can look into why it's not working.  Look at the included unit test code I uploaded for testing the InvoiceQueryRq/InvoiceAddRq/InvoiceModRq QBXML calls to see how I use the dll to generate the QBXML and then convert the QBXML result string back into a C# object (in this case QbInvoicesView).

Another note, I changed the target framework from netcoreapp3.1 to netstandard2.1. This should make it more compatible with any projects you incorporate this into.  Theoretically, this should make it compatible with other types of projects beyond Windows apps.
