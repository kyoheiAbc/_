
/*
 *    MCreator note: This file will be REGENERATED on each build.
 */
package net.mcreator.ninjago.init;

import net.minecraftforge.registries.RegistryObject;
import net.minecraftforge.registries.ForgeRegistries;
import net.minecraftforge.registries.DeferredRegister;
import net.minecraftforge.common.ForgeSpawnEggItem;

import net.minecraft.world.item.Item;
import net.minecraft.world.item.CreativeModeTab;

import net.mcreator.ninjago.NinjagoMod;

public class NinjagoModItems {
	public static final DeferredRegister<Item> REGISTRY = DeferredRegister.create(ForgeRegistries.ITEMS, NinjagoMod.MODID);
	public static final RegistryObject<Item> OVERLORD = REGISTRY.register("overlord_spawn_egg",
			() -> new ForgeSpawnEggItem(NinjagoModEntities.OVERLORD, -1, -1, new Item.Properties().tab(CreativeModeTab.TAB_MISC)));
	public static final RegistryObject<Item> GARMADON = REGISTRY.register("garmadon_spawn_egg",
			() -> new ForgeSpawnEggItem(NinjagoModEntities.GARMADON, -1, -1, new Item.Properties().tab(CreativeModeTab.TAB_MISC)));
}
