interface LoadingSpinnerProps {
  message?: string;
}
const LoadingSpinner: React.FC<LoadingSpinnerProps> = ({
  message = "Đang tải...",
}) => {
  return (
    <div className="p-6 text-center">
      <div className="inline-block animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
      <p className="mt-2 text-gray-600">{message}</p>
    </div>
  );
};
export default LoadingSpinner;
