import { UserLogin } from './userLogin';

export class UserRegistration extends UserLogin {

  constructor(
    public userName: string,
    public password: string,
    public email: string,
    public passwordConfirm: string
  ) {
    super(userName, password);
  }
}
