import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Pagination from '../../components/Pagination/Pagination';
import PostCard from '../../components/PostCard/PostCard';
import { SkeletonPostList } from '../../components/SkeletonLoader/SkeletonLoader';
import ErrorAlert from '../../components/ErrorAlert/ErrorAlert';
import './PostListPage.css';

const PostListPage = () => {
    const [posts, setPosts] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const pageSize = 5;

    const fetchPosts = async (page) => {
        setError(null);
        setLoading(true);
        try {
            const response = await axios.get(`/api/posts?page=${page}&pageSize=${pageSize}`);
            setPosts(response.data);

            const paginationHeader = JSON.parse(response.headers['x-pagination']);
            setTotalPages(paginationHeader.totalPages);
        } catch (err) {
            setError('Failed to load posts. Please try again.');
            console.error("Error fetching posts:", err);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPosts(currentPage);
    }, [currentPage]);

    const handlePageChange = (page) => {
        setCurrentPage(page);
    };

    if (loading) {
        return (
            <div className="post-list-container">
                <h1>Latest Posts</h1>
                <div className="posts-grid">
                    <SkeletonPostList count={pageSize} />
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="post-list-container">
                <h1>Latest Posts</h1>
                <ErrorAlert
                    message={error}
                    onRetry={() => fetchPosts(currentPage)}
                />
            </div>
        );
    }

    return (
        <div className="post-list-container">
            <h1>Latest Posts</h1>
            {posts.length === 0 ? (
                <div className="no-posts">
                    <h2>No posts found</h2>
                    <p>Be the first to create a post!</p>
                </div>
            ) : (
                <>
                    <div className="posts-grid">
                        {posts.map(post => (
                            <PostCard key={post.id} post={post} />
                        ))}
                    </div>
                    {totalPages > 1 && (
                        <Pagination
                            currentPage={currentPage}
                            totalPages={totalPages}
                            onPageChange={handlePageChange}
                        />
                    )}
                </>
            )}
        </div>
    );
};

export default PostListPage;
