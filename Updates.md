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
