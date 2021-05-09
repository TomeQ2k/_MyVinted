import { Guid } from "guid-typescript";
import { FileModel } from "../models/helpers/file-model";

export const appendPhotos = (photoModels: FileModel[], files: File[]) => {
  for (const file of files) {
    const reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onload = () => {
      photoModels.push(new FileModel(reader.result.toString(), file));
    };
  }

  return photoModels;
};

export const removePhoto = (photoModels: FileModel[], id: Guid) => photoModels.filter(p => p.id !== id);
