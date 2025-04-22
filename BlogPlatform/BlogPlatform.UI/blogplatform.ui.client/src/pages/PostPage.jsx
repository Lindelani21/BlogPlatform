import LikeButton from '../../components/LikeButton/LikeButton';
import CommentSection from '../../components/CommentSection/CommentSection';

function PostPage() {
    return (
        <div className="post-page">
            {/* Post content here */}
            <LikeButton postId={post.id} />
            <CommentSection postId={post.id} />
        </div>
    );
}