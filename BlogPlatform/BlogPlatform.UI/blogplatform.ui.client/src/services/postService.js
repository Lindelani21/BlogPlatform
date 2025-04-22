import api from './api';

export const getPost = (id) => api.get(`/posts/${id}`);
export const createPost = (postData) => api.post('/posts', postData);
export const getComments = (postId) => api.get(`/posts/${postId}/comments`);
export const addComment = (postId, content) =>
    api.post(`/posts/${postId}/comments`, { content });
export const toggleLike = (postId) =>
    api.post(`/posts/${postId}/likes`);