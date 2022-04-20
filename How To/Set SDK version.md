# Setting specific SDK API version

By default, the SDK version number is set to "15.0".  When working with older versions of QuickBooks, you may need to set the SDK API version to a different version.  The QbHelpers namespace contains the XmlHelper.SetSdkVersion method.  Use this method to change the QBXML version number used.

The below code will set the version number to "13.0".

```
QbHelpers.XmlHelper.SetSdkVersion("13.0");
```

The resulting QBXML will look like:
```
<?xml version="1.0" encoding="utf-16"?>
<?qbxml version="13.0"?>
<QBXML>
  <QBXMLMsgsRq onError="stopOnError">
    ...
  </QBXMLMsgsRq>
</QBXML>
```
