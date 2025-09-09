import { useForm } from "react-hook-form";
import type { ResetPasswordRequest } from "../../types/auth";
import { useNavigate } from "react-router-dom";
import { authService } from "../../services/authService";
import { toast } from "react-toastify";
import FormInput from "../../components/FormInputs";

const ResetPasswordPage: React.FC = () => {
    const {register, handleSubmit, formState: { errors, isSubmitting },} = useForm<ResetPasswordRequest>();
    const navigate = useNavigate();
    const onSubmit = async (data: ResetPasswordRequest) => {
        // Xử lý đặt lại mật khẩu
        try {
            await authService.resetPassword(data.token, data.newPassword,data.confirmNewPassword);
            toast.success("Đặt lại mật khẩu thành công. Vui lòng đăng nhập lại.");
            navigate('/login');
        } catch (error) {
            if (error instanceof Error) {
                toast.error(error.message); // Hiển thị đúng thông báo từ backend
            } else {
                toast.error('Đã xảy ra lỗi không xác định!');
            }   
        }
    };
    return (
        <div className="min-h-screen flex items-center justify-center px-4 relative">
    <div
      className="absolute inset-0 bg-cover bg-center bg-no-repeat"
      style={{
        backgroundImage: `url('https://bcp.cdnchinhphu.vn/Uploaded/hoangtrongdien/2020_04_07/thu%20vien.jpg')`,
      }}
    />
    <div className="absolute inset-0 bg-black/60" />

    <div className="relative z-10 w-full max-w-md bg-white/20 backdrop-blur-md rounded-xl shadow-lg p-8">
      <h1 className="text-4xl font-bold text-white mb-6 text-center">Đặt lại mật khẩu</h1>

      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        <FormInput id="Password" label="Mật khẩu*" type="password" register={register("newPassword",{required: "Mật khẩu là bắt buộc",minLength: {value: 6,message: "Mật khẩu phải có ít nhất 6 ký tự",},})} error={errors.newPassword}/>
        <FormInput id="ConfirmPassword" label="Xác nhận mật khẩu" type="password" register={register("confirmNewPassword",{required: "Xác nhận mật khẩu là bắt buộc",validate: (value) => value === (document.getElementById('Password') as HTMLInputElement)?.value || "Mật khẩu không khớp",})} error={errors.confirmNewPassword}/>
            
        <button
          type="submit"
          disabled={isSubmitting}
          className="w-full bg-blue-600 text-white py-2 rounded-lg hover:bg-blue-700 disabled:bg-blue-400 transition duration-300"
        >
          {isSubmitting ? "Đang đặt lại..." : "Đặt lại mật khẩu"}
        </button>
      </form>

      <p className="mt-6 text-center text-white">
        Chưa có tài khoản?{" "}
        <a href="/register" className="text-blue-300 hover:underline">
          Đăng ký
        </a>
      </p>
    </div>
  </div>
    );
};
export default ResetPasswordPage;