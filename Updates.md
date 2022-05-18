<i>5/18/2022</i><br />
Went down another rabbit hole.  Took QbModels and created QbModels.QBO for generating/reading  XML and/or JSON for Quickbooks Online Edition.  Going on vacation so documentation will follow at a later date.  For now, look at the QbProcessor repository and review the Quickbooks Online Processor and Test projects to review.

<i>4/17/2022</i><br />
Made major changes to the serialization process.  As such, am changing the version number to 1.2.x to synchronize between NuGet and GitHub.

Completely rewrote the serialization process to use IXmlSerializable instead of relying solely on XmlSerializer.  This allows me to have greater control over the serialization process.  Because of this, have eliminated all the Overload and Specified properties that were required to make the XmlSerializer work properly resulting in fewer and cleaner properties.  Also used ENUM properties where appropriate.  This would have been done from the onset but the XmlSerializer was having issues with this.

On another note, decided to deprecate all Qb{request type}View model views.  Decided instead to start naming the views {request type}Rs.  The Qb...View is still there but will be removed some time in the future.  This also makes for cleaner coding (IMO).  For example, for Invoice request types, the intellisense should list:
```
InvoiceAddRq
InvoiceModRq
InvoiceQueryRq
InvoiceRetDto
InvoiceRs
```

<i>4/11/2022</i><br />
Added ListDel/ListDeleted/ListDisplayAdd/ListDisplayMod requests.
In the process of adding ListDel request, found bugs with the following:
<ul><li>AccountAddRq</li><li>TxnDelRq</li></ul>
Added the following views that were missing:
<ul><li>QbVehiclesView</li><li>QbVehicleMileageView</li></ul>

Uploaded my Unit Tests project as well as the class library project I created to implement the QBXMLRP2Lib class.  The QbProcessor class library is used to send and receive QBXML requests and responses from the Quickbooks RP.

<i>4/9/2022</i><br />
Updated the XML documentation for all the Rq objects.  It's a minor change.  Assembly file version 1.1.11.

<i>4/8/2022</i><br />
Updated XML documentation for all properties and classes.  Have not tested accuracy because there are probably over 1,000 properties and classes to check.  But I spot checked my existing code base and the intellisense came through.  As of today, have not updated NuGet yet.  Assembly file version 1.1.10.

<i>4/7/2022</i><br />
Still going down the rabbit hole.  I have updated all the Rq classes and Qb_View classes.  The only thing remaining are the returned Dtos.  

<i>4/5/2022</i><br />
Realized that the XML document was not coming through.  Resolved my build process but now I am going down the rabbit hole of XML documentation.  There are a lot of documentation to do so I will be releasing this parts as I go along.  Will be a while.
