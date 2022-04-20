# Request Validation

Each QbModel request object will have 2 extensions to assist you with creating properly formed QBXML.  These extensions are **IsEntityValid()** and **GetErrorsList()**.  If the QbModel request is not valid, the IsEntityValid() will return false.  You can see the individual errors by calling the GetErrorsList() extension.  For example, the following code will produce invalid validation responses.

```csharp
InvoiceAddRq invoiceRq = new();
invoiceRq.InvoiceLine = new();
invoiceRq.InvoiceGroupLine = new();
Console.WriteLine(invoiceRq.IsEntityValid());
```

Will output false for the invoiceRq.IsEntityValid() method.  A subsequent call to invoiceRq.GetErrorsList() will produce the following output.

| Name | Value |
| ----------- | ----------- |
| invoiceRq.GetErrorsList() | Count = 4 |
| 0 | "CustomerRef is required" | 
| 1 | "More than one of InvoiceLine\|InvoiceGroupLine have a value." | 
| 2 | "More than one of InvoiceLine\|InvoiceGroupLine have a value." | 
| 3 | "ItemGroup is required" | 


![image](https://user-images.githubusercontent.com/56395390/164131518-77b14a05-a1f8-468b-8eb8-2d155b26d7a0.png)

From the GetErrorsList(), you can see that the Customer object was not created and assigned a value.  You can also see that InvoiceLine cannot be combined with InvoiceGroupLine.  Lastly, if InvoiceGroupLine is to be used, the ItemGroup is required.

> <b>NOTE: </b>Other than ClassRef, all QBXML Ref objects will not have the Ref extension on the property.  In this case, the error "CustomerRef is required" will refer to the invoiceRq.Customer object.

