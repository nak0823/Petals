using System;

namespace Petals.CLI.Models
{
    public interface ILogger
    {
        void ShowInfo(string message, bool sleep);

        void ShowSuccess(string message, bool sleep);

        void ShowError(string message, bool sleep);

        void ShowWarning(string message, bool sleep);

        void ShowException(Exception exception, bool sleep);
    }
}