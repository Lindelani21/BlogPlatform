import React from 'react';
import './Pagination.css';

/**
 * @param {{ currentPage: number, totalPages: number, onPageChange: (page: number) => void }}
 */
const Pagination = ({ currentPage, totalPages, onPageChange }) => {
    const pageNumbers = [];
    const maxVisiblePages = 5;

    let startPage = Math.max(1, currentPage - Math.floor(maxVisiblePages / 2));
    let endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);

    if (endPage - startPage + 1 < maxVisiblePages) {
        startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }

    for (let i = startPage; i <= endPage; i++) {
        pageNumbers.push(i);
    }

    return (
        <div className="pagination">
            <button
                onClick={() => onPageChange(1)}
                disabled={currentPage === 1}
            >
                &laquo;
            </button>
            <button
                onClick={() => onPageChange(currentPage - 1)}
                disabled={currentPage === 1}
            >
                &lsaquo;
            </button>

            {startPage > 1 && (
                <>
                    <button onClick={() => onPageChange(1)}>1</button>
                    {startPage > 2 && <span className="ellipsis">...</span>}
                </>
            )}

            {pageNumbers.map(number => (
                <button
                    key={number}
                    onClick={() => onPageChange(number)}
                    className={currentPage === number ? 'active' : ''}
                >
                    {number}
                </button>
            ))}

            {endPage < totalPages && (
                <>
                    {endPage < totalPages - 1 && <span className="ellipsis">...</span>}
                    <button onClick={() => onPageChange(totalPages)}>
                        {totalPages}
                    </button>
                </>
            )}

            <button
                onClick={() => onPageChange(currentPage + 1)}
                disabled={currentPage === totalPages}
            >
                &rsaquo;
            </button>
            <button
                onClick={() => onPageChange(totalPages)}
                disabled={currentPage === totalPages}
            >
                &raquo;
            </button>
        </div>
    );
};

export default Pagination;
