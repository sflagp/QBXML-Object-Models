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

I've also added a couple of validation extensions using DataAnnotations and custom ValidationAttributes to be able to call IsEntityValid() and/or GetErrorsList() on the object.

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
