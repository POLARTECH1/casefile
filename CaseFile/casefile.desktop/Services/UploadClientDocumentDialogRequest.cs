using System;

namespace casefile.desktop.Services;

public sealed record UploadClientDocumentDialogRequest(Guid ClientId, string NomDossier);
