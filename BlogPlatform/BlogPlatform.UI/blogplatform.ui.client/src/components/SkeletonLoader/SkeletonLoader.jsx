import React from 'react';
import './SkeletonLoader.css';

const SkeletonLoader = () => {
    return (
        <div className="skeleton-post">
            <div className="skeleton-title"></div>
            <div className="skeleton-content"></div>
            <div className="skeleton-footer"></div>
        </div>
    );
};

export const SkeletonPostList = ({ count }) => {
    return (
        <>
            {Array.from({ length: count }).map((_, index) => (
                <SkeletonLoader key={index} />
            ))}
        </>
    );
};

export default SkeletonLoader;
