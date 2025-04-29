import React from 'react';
import './ErrorAlert.css';

const ErrorAlert = ({ message, onRetry }) => {
    return (
        <div className="error-alert">
            <div className="error-message">{message}</div>
            {onRetry && (
                <button
                    className="retry-button"
                    onClick={onRetry}
                >
                    Retry
                </button>
            )}
        </div>
    );
};

export default ErrorAlert;
