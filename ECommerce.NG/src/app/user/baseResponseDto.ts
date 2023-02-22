import { ErrorDto } from "./errorDto";

export interface BaseResponseDto {
  status: number;
  errors: ErrorDto[];
  result: string;
}
