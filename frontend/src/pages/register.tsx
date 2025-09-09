import { useNavigate } from "react-router-dom";
import type { AuthError, ResgisterCre } from "../types/auth";
import { authService } from "../services/authService";
import { toast } from "react-toastify";
import { useForm } from "react-hook-form";
import type { AxiosError } from "axios";
import FormInput from "../components/FormInputs";
import FormSelect from "../components/FormSelect";

const RegisterPage: React.FC = () => {
    const{register, handleSubmit, formState: {errors, isSubmitting},} = useForm<ResgisterCre>();
    const navigate = useNavigate();

    const onSubmit = async (data: ResgisterCre) => {
        try{
                await authService.register(data);
                toast.success('Đăng ký thành công! Vui lòng đăng nhập.');
                navigate('/login');
        }catch(error){
            const axiosError = error as AxiosError<AuthError>;
            toast.error(axiosError.response?.data.message || 'Đăng ký thất bại!');
        }
    }

    return(
          <div className="min-h-screen flex items-center justify-center px-4 relative">
    <div className="absolute inset-0 bg-cover bg-center bg-no-repeat"
      style={{
        backgroundImage: `url('https://bcp.cdnchinhphu.vn/Uploaded/hoangtrongdien/2020_04_07/thu%20vien.jpg')`,
      }}
    />
    <div className="absolute inset-0 bg-black/60" />
    <div className="relative z-10 w-full max-w-md bg-white/20 backdrop-blur-md rounded-xl shadow-lg p-8">
      <h1 className="text-4xl font-bold text-white mb-6 text-center">Đăng Ký</h1>
        <form onSubmit={handleSubmit(onSubmit)}>
            <FormInput id="UserName" label="Tên đăng nhập*" type="text" register={register("UserName",{required: "Tên đăng nhập là bắt buộc",})} error={errors.UserName}/>
            <FormInput id="Fullname" label="Họ và tên*" type="text" register={register("Fullname",{required: "Họ và tên là bắt buộc",})} error={errors.Fullname}/>
            <FormInput id="PhoneNumber" label="Số điện thoại*" type="text" register={register("PhoneNumber",{required: "Số điện thoại là bắt buộc",pattern: {value: /^\d{10,15}$/,message: "Số điện thoại không hợp lệ",},})} error={errors.PhoneNumber}/>
            <FormInput id="Email" label="Email*" type="email" register={register("Email",{required: "Email là bắt buộc",pattern: {value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,message: "Email không hợp lệ",},})} error={errors.Email}/>
            <FormInput id="Address" label="Địa chỉ*" type="text" register={register("Address",{required: "Địa chỉ là bắt buộc",})} error={errors.Address}/>
            <FormInput id="DOB" label="Ngày sinh" type="date" register={register("DOB",{required: "Ngày sinh là bắt buộc",})} error={errors.DOB}/>
            <FormSelect id="Role" label="Vai trò*" options={[{value: 'User', label: 'Người dùng'},{value: 'Administrator', label: 'Quản trị viên'},{value: 'Manager', label: 'Quản lý'},]} register={register("Role",{required: "Vai trò là bắt buộc",})} error={errors.Role}/>
            <FormInput id="Password" label="Mật khẩu*" type="password" register={register("Password",{required: "Mật khẩu là bắt buộc",minLength: {value: 6,message: "Mật khẩu phải có ít nhất 6 ký tự",},})} error={errors.Password}/>
            <FormInput id="ConfirmPassword" label="Xác nhận mật khẩu" type="password" register={register("ConfirmPassword",{required: "Xác nhận mật khẩu là bắt buộc",validate: (value) => value === (document.getElementById('Password') as HTMLInputElement)?.value || "Mật khẩu không khớp",})} error={errors.ConfirmPassword}/>
            <button type="submit" disabled={isSubmitting} className="w-full bg-blue-500 text-white p-2 rounded-lg hover:bg-blue-600 disabled:bg-blue-300">
                {isSubmitting ? "Đang đăng ký..." : "Đăng Ký"}
            </button>
        </form>
        <p className="mt-4 text-center">
          Đã có tài khoản?{" "}
          <a href="/login" className="text-blue-500 hover:underline">
            Đăng nhập
          </a>
        </p>
      </div>
    </div>
    );
};

export default RegisterPage;