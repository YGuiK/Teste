// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.PowerPlatform.Formulas.Tools;
using Microsoft.PowerPlatform.Formulas.Tools.IR;
using Microsoft.PowerPlatform.Formulas.Tools.Parser;
using System;

namespace Microsoft.PowerPlatform.Formulas.Tools
{
    /// <summary>
    /// Error codes from reading, writing (compiling) a document.
    /// These numbers must stay stable. 
    /// </summary>
    internal enum ErrorCode
    {
        // Warnings start at 2000
        ChecksumMismatch = 2001,

        // Catch-all - review and remove. 
        GenericWarning = 2999,

        // Errors start at 3000
        InternalError = 3001,

        FormatNotSupported = 3002,

        // Parse error 
        ParseError = 3003,

        // This operation isn't supported, use studio
        UnsupportedUseStudio = 3004,

        // Missing properties or values where they're required
        Validation = 3005,

        // Editor State file is corrupt. Delete it. 
        IllegalEditorState = 3006,

        // Sanity check on the final msapp fails.
        // This is a generic error. There's nothing the user can do here (it's the msapp)
        // We should have caught it sooner and issues a more actionable source-level error. 
        MsAppError = 3007,

        // A symbol is already defined.
        // Common for duplicate controls. This suggests an offline edit. 
        DuplicateSymbol = 3008,

        // Catch-all.  Should review and make these more specific. 
        Generic = 3999,

    }

    internal static class ErrorCodeExtensions
    {
        public static bool IsError(this ErrorCode code)
        {
            return (int)code > 3000;
        }

        public static void ChecksumMismatch(this ErrorContainer errors, string message)
        {
            errors.AddError(ErrorCode.ChecksumMismatch, default(SourceLocation), $"Checksum mismatch. {message}");
        }

        public static void GenericWarning(this ErrorContainer errors, string message)
        {
            errors.AddError(ErrorCode.GenericWarning, default(SourceLocation), message);
        }

        public static void FormatNotSupported(this ErrorContainer errors, string message)
        {
            errors.AddError(ErrorCode.FormatNotSupported, default(SourceLocation), $"Format is not supported. {message}");
        }

        public static void GenericError(this ErrorContainer errors, string message)
        {
            errors.AddError(ErrorCode.Generic, default(SourceLocation), message);
        }

        public static void InternalError(this ErrorContainer errors, Exception e)
        {
            errors.AddError(ErrorCode.InternalError, default(SourceLocation), $"Internal error. {e.Message}");
        }

        public static void UnsupportedError(this ErrorContainer errors, string message)
        {
            errors.AddError(ErrorCode.UnsupportedUseStudio, default(SourceLocation), $"Unsupported operation error. {message}");
        }

        public static void EditorStateError(this ErrorContainer errors, SourceLocation loc, string message)
        {
            errors.AddError(ErrorCode.IllegalEditorState, loc, $"Illegal editorstate file. {message}");
        }

        public static void GenericMsAppError(this ErrorContainer errors, string message)
        {
            errors.AddError(ErrorCode.MsAppError, default(SourceLocation), $"MsApp is corrupted. {message}");
        }

        public static void DuplicateSymbolError(this ErrorContainer errors, SourceLocation loc, string message, SourceLocation loc2)
        {
            errors.AddError(ErrorCode.DuplicateSymbol, loc, $"Symbol '{message}' is already defined. Previously at {loc2}");
        }

        public static void ParseError(this ErrorContainer errors, SourceLocation span, string message)
        {
            errors.AddError(ErrorCode.ParseError, span, $"Parse error: {message}");
        }

        public static void ValidationError(this ErrorContainer errors, string message)
        {
            errors.AddError(ErrorCode.Validation, default(SourceLocation), $"Validation error: {message}");
        }
    }
}
