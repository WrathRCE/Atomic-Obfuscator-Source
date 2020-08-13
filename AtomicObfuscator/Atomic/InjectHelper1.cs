using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Atomic
{
	// Token: 0x02000014 RID: 20
	internal class InjectHelper1
	{
		// Token: 0x02000028 RID: 40
		public static class InjectHelper
		{
			// Token: 0x060000D8 RID: 216 RVA: 0x00009474 File Offset: 0x00007674
			private static TypeDefUser Clone(TypeDef origin)
			{
				TypeDefUser typeDefUser = new TypeDefUser(origin.Namespace, origin.Name);
				typeDefUser.Attributes = origin.Attributes;
				bool flag = origin.ClassLayout != null;
				if (flag)
				{
					typeDefUser.ClassLayout = new ClassLayoutUser(origin.ClassLayout.PackingSize, origin.ClassSize);
				}
				foreach (GenericParam genericParam in origin.GenericParameters)
				{
					typeDefUser.GenericParameters.Add(new GenericParamUser(genericParam.Number, genericParam.Flags, "-"));
				}
				return typeDefUser;
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x00009534 File Offset: 0x00007734
			private static MethodDefUser Clone(MethodDef origin)
			{
				MethodDefUser methodDefUser = new MethodDefUser(origin.Name, null, origin.ImplAttributes, origin.Attributes);
				foreach (GenericParam genericParam in origin.GenericParameters)
				{
					methodDefUser.GenericParameters.Add(new GenericParamUser(genericParam.Number, genericParam.Flags, "-"));
				}
				return methodDefUser;
			}

			// Token: 0x060000DA RID: 218 RVA: 0x000095C4 File Offset: 0x000077C4
			private static FieldDefUser Clone(FieldDef origin)
			{
				return new FieldDefUser(origin.Name, null, origin.Attributes);
			}

			// Token: 0x060000DB RID: 219 RVA: 0x000095EC File Offset: 0x000077EC
			private static TypeDef PopulateContext(TypeDef typeDef, InjectHelper1.InjectHelper.InjectContext ctx)
			{
				IDnlibDef dnlibDef;
				bool flag = !ctx.Map.TryGetValue(typeDef, out dnlibDef);
				TypeDef typeDef2;
				if (flag)
				{
					typeDef2 = InjectHelper1.InjectHelper.Clone(typeDef);
					ctx.Map[typeDef] = typeDef2;
				}
				else
				{
					typeDef2 = (TypeDef)dnlibDef;
				}
				foreach (TypeDef typeDef3 in typeDef.NestedTypes)
				{
					typeDef2.NestedTypes.Add(InjectHelper1.InjectHelper.PopulateContext(typeDef3, ctx));
				}
				foreach (MethodDef methodDef in typeDef.Methods)
				{
					typeDef2.Methods.Add((MethodDef)(ctx.Map[methodDef] = InjectHelper1.InjectHelper.Clone(methodDef)));
				}
				foreach (FieldDef fieldDef in typeDef.Fields)
				{
					typeDef2.Fields.Add((FieldDef)(ctx.Map[fieldDef] = InjectHelper1.InjectHelper.Clone(fieldDef)));
				}
				return typeDef2;
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00009758 File Offset: 0x00007958
			private static void CopyTypeDef(TypeDef typeDef, InjectHelper1.InjectHelper.InjectContext ctx)
			{
				TypeDef typeDef2 = (TypeDef)ctx.Map[typeDef];
				typeDef2.BaseType = (ITypeDefOrRef)ctx.Importer.Import(typeDef.BaseType);
				foreach (InterfaceImpl interfaceImpl in typeDef.Interfaces)
				{
					typeDef2.Interfaces.Add(new InterfaceImplUser((ITypeDefOrRef)ctx.Importer.Import(interfaceImpl.Interface)));
				}
			}

			// Token: 0x060000DD RID: 221 RVA: 0x00009800 File Offset: 0x00007A00
			private static void CopyMethodDef(MethodDef methodDef, InjectHelper1.InjectHelper.InjectContext ctx)
			{
				MethodDef methodDef2 = (MethodDef)ctx.Map[methodDef];
				methodDef2.Signature = ctx.Importer.Import(methodDef.Signature);
				methodDef2.Parameters.UpdateParameterTypes();
				bool flag = methodDef.ImplMap != null;
				if (flag)
				{
					methodDef2.ImplMap = new ImplMapUser(new ModuleRefUser(ctx.TargetModule, methodDef.ImplMap.Module.Name), methodDef.ImplMap.Name, methodDef.ImplMap.Attributes);
				}
				foreach (CustomAttribute customAttribute in methodDef.CustomAttributes)
				{
					methodDef2.CustomAttributes.Add(new CustomAttribute((ICustomAttributeType)ctx.Importer.Import(customAttribute.Constructor)));
				}
				bool hasBody = methodDef.HasBody;
				if (hasBody)
				{
					methodDef2.Body = new CilBody(methodDef.Body.InitLocals, new List<Instruction>(), new List<ExceptionHandler>(), new List<Local>());
					methodDef2.Body.MaxStack = methodDef.Body.MaxStack;
					Dictionary<object, object> bodyMap = new Dictionary<object, object>();
					foreach (Local local in methodDef.Body.Variables)
					{
						Local local2 = new Local(ctx.Importer.Import(local.Type));
						methodDef2.Body.Variables.Add(local2);
						local2.Name = local.Name;
						local2.PdbAttributes = local.PdbAttributes;
						bodyMap[local] = local2;
					}
					foreach (Instruction instruction in methodDef.Body.Instructions)
					{
						Instruction instruction2 = new Instruction(instruction.OpCode, instruction.Operand);
						instruction2.SequencePoint = instruction.SequencePoint;
						bool flag2 = instruction2.Operand is IType;
						if (flag2)
						{
							instruction2.Operand = ctx.Importer.Import((IType)instruction2.Operand);
						}
						else
						{
							bool flag3 = instruction2.Operand is IMethod;
							if (flag3)
							{
								instruction2.Operand = ctx.Importer.Import((IMethod)instruction2.Operand);
							}
							else
							{
								bool flag4 = instruction2.Operand is IField;
								if (flag4)
								{
									instruction2.Operand = ctx.Importer.Import((IField)instruction2.Operand);
								}
							}
						}
						methodDef2.Body.Instructions.Add(instruction2);
						bodyMap[instruction] = instruction2;
					}
					Func<Instruction, Instruction> <>9__0;
					foreach (Instruction instruction3 in methodDef2.Body.Instructions)
					{
						bool flag5 = instruction3.Operand != null && bodyMap.ContainsKey(instruction3.Operand);
						if (flag5)
						{
							instruction3.Operand = bodyMap[instruction3.Operand];
						}
						else
						{
							bool flag6 = instruction3.Operand is Instruction[];
							if (flag6)
							{
								Instruction instruction4 = instruction3;
								IEnumerable<Instruction> source = (Instruction[])instruction3.Operand;
								Func<Instruction, Instruction> selector;
								if ((selector = <>9__0) == null)
								{
									selector = (<>9__0 = ((Instruction target) => (Instruction)bodyMap[target]));
								}
								instruction4.Operand = source.Select(selector).ToArray<Instruction>();
							}
						}
					}
					foreach (ExceptionHandler exceptionHandler in methodDef.Body.ExceptionHandlers)
					{
						methodDef2.Body.ExceptionHandlers.Add(new ExceptionHandler(exceptionHandler.HandlerType)
						{
							CatchType = ((exceptionHandler.CatchType == null) ? null : ((ITypeDefOrRef)ctx.Importer.Import(exceptionHandler.CatchType))),
							TryStart = (Instruction)bodyMap[exceptionHandler.TryStart],
							TryEnd = (Instruction)bodyMap[exceptionHandler.TryEnd],
							HandlerStart = (Instruction)bodyMap[exceptionHandler.HandlerStart],
							HandlerEnd = (Instruction)bodyMap[exceptionHandler.HandlerEnd],
							FilterStart = ((exceptionHandler.FilterStart == null) ? null : ((Instruction)bodyMap[exceptionHandler.FilterStart]))
						});
					}
					methodDef2.Body.SimplifyMacros(methodDef2.Parameters);
				}
			}

			// Token: 0x060000DE RID: 222 RVA: 0x00009D94 File Offset: 0x00007F94
			private static void CopyFieldDef(FieldDef fieldDef, InjectHelper1.InjectHelper.InjectContext ctx)
			{
				FieldDef fieldDef2 = (FieldDef)ctx.Map[fieldDef];
				fieldDef2.Signature = ctx.Importer.Import(fieldDef.Signature);
			}

			// Token: 0x060000DF RID: 223 RVA: 0x00009DD0 File Offset: 0x00007FD0
			private static void Copy(TypeDef typeDef, InjectHelper1.InjectHelper.InjectContext ctx, bool copySelf)
			{
				if (copySelf)
				{
					InjectHelper1.InjectHelper.CopyTypeDef(typeDef, ctx);
				}
				foreach (TypeDef typeDef2 in typeDef.NestedTypes)
				{
					InjectHelper1.InjectHelper.Copy(typeDef2, ctx, true);
				}
				foreach (MethodDef methodDef in typeDef.Methods)
				{
					InjectHelper1.InjectHelper.CopyMethodDef(methodDef, ctx);
				}
				foreach (FieldDef fieldDef in typeDef.Fields)
				{
					InjectHelper1.InjectHelper.CopyFieldDef(fieldDef, ctx);
				}
			}

			// Token: 0x060000E0 RID: 224 RVA: 0x00009EB8 File Offset: 0x000080B8
			public static TypeDef Inject(TypeDef typeDef, ModuleDef target)
			{
				InjectHelper1.InjectHelper.InjectContext injectContext = new InjectHelper1.InjectHelper.InjectContext(typeDef.Module, target);
				InjectHelper1.InjectHelper.PopulateContext(typeDef, injectContext);
				InjectHelper1.InjectHelper.Copy(typeDef, injectContext, true);
				return (TypeDef)injectContext.Map[typeDef];
			}

			// Token: 0x060000E1 RID: 225 RVA: 0x00009EFC File Offset: 0x000080FC
			public static MethodDef Inject(MethodDef methodDef, ModuleDef target)
			{
				InjectHelper1.InjectHelper.InjectContext injectContext = new InjectHelper1.InjectHelper.InjectContext(methodDef.Module, target);
				injectContext.Map[methodDef] = InjectHelper1.InjectHelper.Clone(methodDef);
				InjectHelper1.InjectHelper.CopyMethodDef(methodDef, injectContext);
				return (MethodDef)injectContext.Map[methodDef];
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x00009F48 File Offset: 0x00008148
			public static IEnumerable<IDnlibDef> Inject(TypeDef typeDef, TypeDef newType, ModuleDef target)
			{
				InjectHelper1.InjectHelper.InjectContext injectContext = new InjectHelper1.InjectHelper.InjectContext(typeDef.Module, target);
				injectContext.Map[typeDef] = newType;
				InjectHelper1.InjectHelper.PopulateContext(typeDef, injectContext);
				InjectHelper1.InjectHelper.Copy(typeDef, injectContext, false);
				return injectContext.Map.Values.Except(new TypeDef[]
				{
					newType
				});
			}

			// Token: 0x02000038 RID: 56
			private class InjectContext : ImportResolver
			{
				// Token: 0x0600010D RID: 269 RVA: 0x0000AF09 File Offset: 0x00009109
				public InjectContext(ModuleDef module, ModuleDef target)
				{
					this.OriginModule = module;
					this.TargetModule = target;
					this.importer = new Importer(target, ImporterOptions.TryToUseTypeDefs);
					this.importer.Resolver = this;
				}

				// Token: 0x1700001C RID: 28
				// (get) Token: 0x0600010E RID: 270 RVA: 0x0000AF48 File Offset: 0x00009148
				public Importer Importer
				{
					get
					{
						return this.importer;
					}
				}

				// Token: 0x0600010F RID: 271 RVA: 0x0000AF60 File Offset: 0x00009160
				public override TypeDef Resolve(TypeDef typeDef)
				{
					bool flag = this.Map.ContainsKey(typeDef);
					TypeDef result;
					if (flag)
					{
						result = (TypeDef)this.Map[typeDef];
					}
					else
					{
						result = null;
					}
					return result;
				}

				// Token: 0x06000110 RID: 272 RVA: 0x0000AF98 File Offset: 0x00009198
				public override MethodDef Resolve(MethodDef methodDef)
				{
					bool flag = this.Map.ContainsKey(methodDef);
					MethodDef result;
					if (flag)
					{
						result = (MethodDef)this.Map[methodDef];
					}
					else
					{
						result = null;
					}
					return result;
				}

				// Token: 0x06000111 RID: 273 RVA: 0x0000AFD0 File Offset: 0x000091D0
				public override FieldDef Resolve(FieldDef fieldDef)
				{
					bool flag = this.Map.ContainsKey(fieldDef);
					FieldDef result;
					if (flag)
					{
						result = (FieldDef)this.Map[fieldDef];
					}
					else
					{
						result = null;
					}
					return result;
				}

				// Token: 0x04000084 RID: 132
				public readonly Dictionary<IDnlibDef, IDnlibDef> Map = new Dictionary<IDnlibDef, IDnlibDef>();

				// Token: 0x04000085 RID: 133
				public readonly ModuleDef OriginModule;

				// Token: 0x04000086 RID: 134
				public readonly ModuleDef TargetModule;

				// Token: 0x04000087 RID: 135
				private readonly Importer importer;
			}
		}
	}
}
