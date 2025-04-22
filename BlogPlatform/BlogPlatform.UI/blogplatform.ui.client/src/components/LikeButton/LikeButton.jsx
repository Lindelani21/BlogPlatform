import { useState, useEffect } from 'react';
import axios from 'axios';

export default function LikeButton({ postId }) {
    const [likeStatus, setLikeStatus] = useState({
        totalLikes: 0,
        isLikedByCurrentUser: false
    });

    const fetchLikeStatus = async () => {
        const res = await axios.get(`/api/posts/${postId}/likes`);
        setLikeStatus(res.data);
    };

    const toggleLike = async () => {
        await axios.post(`/api/posts/${postId}/likes`);
        fetchLikeStatus();
    };

    useEffect(() => { fetchLikeStatus(); }, [postId]);

    return (
        <button
            onClick={toggleLike}
            className={likeStatus.isLikedByCurrentUser ? 'liked' : ''}
        >
            ♥ {likeStatus.totalLikes}
        </button>
    );
}