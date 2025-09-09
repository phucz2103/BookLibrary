import { useForm } from "react-hook-form";
import type { ForgotPasswordRequest } from "../../types/auth";
import { authService } from "../../services/authService";
import { toast } from "react-toastify";
import FormInput from "../../components/FormInputs";
import { useNavigate } from "react-router-dom";

const ForgotPasswordPage: React.FC = () => {
    const {register, handleSubmit, formState: { errors, isSubmitting },} = useForm<ForgotPasswordRequest>();
    const navaigate = useNavigate();

    const onSubmit = async (data: ForgotPasswordRequest) => {
        // Xử lý gửi yêu cầu quên mật khẩu
        try {
            await authService.forgotPassword(data.Email);
            toast.success("Yêu cầu quên mật khẩu đã được gửi. Vui lòng kiểm tra email của bạn.");
            navaigate('/login');
        } catch (error) {
            if (error instanceof Error) {
                toast.error(error.message); // Hiển thị đúng thông báo từ backend 
            } else {
                toast.error('Đã xảy ra lỗi không xác định!');
            }
        }
    };

    return(
        <div className="min-h-screen flex items-center justify-center px-4 relative">
            <div
              className="absolute inset-0 bg-cover bg-center bg-no-repeat"
              style={{
                backgroundImage: `url('https://bcp.cdnchinhphu.vn/Uploaded/hoangtrongdien/2020_04_07/thu%20vien.jpg')`,
              }}
            />
            <div className="absolute inset-0 bg-black/60" />
        
            <div className="relative z-10 w-full max-w-md bg-white/20 backdrop-blur-md rounded-xl shadow-lg p-8">
              <h1 className="text-4xl font-bold text-white mb-6 text-center"> Quên mật khẩu </h1>
        
              <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
                <FormInput id="Email" label="Email*" type="email" register={register("Email",{required: "Email là bắt buộc",pattern: {value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,message: "Email không hợp lệ",},})} error={errors.Email}/>
                <button
                  type="submit"
                  disabled={isSubmitting}
                  className="w-full bg-blue-600 text-white py-2 rounded-lg hover:bg-blue-700 disabled:bg-blue-400 transition duration-300"
                >
                  {isSubmitting ? "Đang gửi..." : "Gửi Yêu Cầu"}
                </button>
              </form>
              <p className="mt-4 text-center">
          <a href="/login" className="text-blue-500 hover:underline">
            Quay lại đăng nhập
          </a>
        </p>
            </div>
          </div>
    )
}                        
export default ForgotPasswordPage;