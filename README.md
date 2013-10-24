FileUploadSP
============

SharePoint 2010 file uploading DEMO with SP.UI modal and jQuery AJAX. No postbacks. Multiple files.

No fancy checking or error management, just bare bones to check out how it works.

Deployment:

1. Create document library "Temporal". If not same name, change constant in DAL.SharePoint2010.cs.
2. Create list "Documentos". If not same name, change constant in DAL.SharePoint2010.cs
3. Modify constant in DAL.SharePoint2010.cs and have it point to the SP Web where it's going to get deployed.
4. Check that the there is a SitePage library, and edit URL in the Module if necessary.
5. Deploy with VS or with stsadm/powrshell.
